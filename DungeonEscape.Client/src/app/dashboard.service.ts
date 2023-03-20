import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable, ReplaySubject, scan } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private resultSubject: ReplaySubject<PlayerActionResult> = new ReplaySubject<PlayerActionResult>(1);
  results$: Observable<PlayerActionResult[]> = this.resultSubject.asObservable().pipe(
    scan((acc, cur) => {
      acc.push(cur);
      return acc;
    }, [] as PlayerActionResult[])
  );

  private hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl("***place app url here***" + '/hub/Game')
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
      this.resultSubject.next(gameState.player);
    });
  }
}
