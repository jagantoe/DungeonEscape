import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@microsoft/signalr';
import { Observable, ReplaySubject, scan } from 'rxjs';
import { PlayerActionResult } from './types/player-action-result';

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

  private hubConnection!: HubConnection;

  public connect(token: string) {
    let options: IHttpConnectionOptions = {
      accessTokenFactory: () => token
    };
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7236" + "/hub/Dashboard", options)
      .withAutomaticReconnect()
      .build();
    this.startConnection();
    this.addListeners();
  }

  private startConnection() {
    try {
      this.hubConnection.start();
    } catch (error) {
      console.log('error while establishing signalr connection: ' + error);
    }
  }

  private addListeners() {
    this.hubConnection.on("Game", (actionResult: PlayerActionResult) => {
      this.resultSubject.next(actionResult);
    });
  }
}
