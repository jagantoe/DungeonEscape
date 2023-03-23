import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@microsoft/signalr';
import { map, Observable, ReplaySubject } from 'rxjs';
import { Character } from './types/character';
import { GameState } from './types/gamestate';
import { Inspection } from './types/inspection';
import { Player } from './types/player';
import { Tile } from './types/tile';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private gameStateSubject: ReplaySubject<GameState> = new ReplaySubject<GameState>(1);
  public gameState$: Observable<GameState> = this.gameStateSubject.asObservable();
  public player$: Observable<Player> = this.gameState$.pipe(map(x => x.player));
  public vision$: Observable<Tile[]> = this.gameState$.pipe(map(x => x.vision));
  private inspectionSubject: ReplaySubject<Inspection> = new ReplaySubject<Inspection>(1);
  public inspection$: Observable<Inspection> = this.inspectionSubject.asObservable();

  private options: IHttpConnectionOptions = {
    accessTokenFactory: () => "*** place token here ***"
  };
  private hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl("***place app url here***" + '/hub/Game', this.options)
    .withAutomaticReconnect()
    .build();

  constructor() {
    this.connect();
    this.addListeners();
  }

  private connect() {
    try {
      this.hubConnection.start().catch();
    } catch (error) {
      console.log('error while establishing signalr connection: ' + error);
    }
  }

  private addListeners() {
    this.hubConnection.on("NewRound", (gameState: GameState) => {
      this.gameStateSubject.next(gameState);
    });
    this.hubConnection.on("Inspect", (inspection: Inspection) => {
      this.inspectionSubject.next(inspection);
    });
  }

  public move(x: number, y: number) {
    this.hubConnection.invoke("Move", x, y);
  }
  public interact(x: number, y: number) {
    this.hubConnection.invoke("Interact", x, y);
  }
  public inspect(x: number, y: number) {
    this.hubConnection.invoke("Inspect", x, y);
  }
  public switchCharacter(character: Character) {
    this.hubConnection.invoke("SwitchCharacter", character);
  }
  public reset() {
    this.hubConnection.invoke("Reset");
  }
}
