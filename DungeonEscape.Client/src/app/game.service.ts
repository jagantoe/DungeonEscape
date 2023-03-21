import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, IHttpConnectionOptions } from '@microsoft/signalr';
import { map, Observable, ReplaySubject, timer } from 'rxjs';
import { Character } from './types/character';
import { GameState } from './types/gamestate';
import { Inspection } from './types/inspection';
import { Player } from './types/player';
import { Tile } from './types/tile';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private playerSubject: ReplaySubject<Player> = new ReplaySubject<Player>(1);
  player$: Observable<Player> = this.playerSubject.asObservable();
  private visionSubject: ReplaySubject<Tile[]> = new ReplaySubject<Tile[]>(1);
  vision$: Observable<Tile[]> = this.visionSubject.asObservable();
  private inspectionSubject: ReplaySubject<Inspection> = new ReplaySubject<Inspection>(1);
  inspection$: Observable<Inspection> = this.inspectionSubject.asObservable();

  private token!: string;
  private options!: IHttpConnectionOptions;
  private hubConnection!: HubConnection;
  connected$: Observable<boolean> = timer(1000, 5000).pipe(
    map(x => this.hubConnection?.state == HubConnectionState.Connected)
  );

  public connect(token: string) {
    this.token = token;
    this.options = {
      accessTokenFactory: () => token
    };

    this.startConnection();
    this.addListeners();
  }
  public reconnect() {
    this.startConnection();
  }

  private startConnection() {
    try {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl("https://localhost:7236" + "/hub/Game", this.options)
        .withAutomaticReconnect()
        .build();
      this.hubConnection.start();
    } catch (error) {
      console.log('error while establishing signalr connection: ' + error);
    }
  }

  private addListeners() {
    this.hubConnection.on("NewRound", (gameState: GameState) => {
      this.playerSubject.next(gameState.player);
      this.visionSubject.next(gameState.visionTiles);
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
    this.hubConnection.invoke("SwitchClass", character);
  }
  public reset() {
    this.hubConnection.invoke("Reset");
  }
}
