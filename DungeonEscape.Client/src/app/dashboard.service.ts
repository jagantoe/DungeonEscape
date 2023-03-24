import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, IHttpConnectionOptions } from '@microsoft/signalr';
import { map, Observable, ReplaySubject, scan, shareReplay, timer } from 'rxjs';
import { PlayerActionResult } from './types/player-action-result';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private resultSubject: ReplaySubject<PlayerActionResult> = new ReplaySubject<PlayerActionResult>(1);
  results$: Observable<PlayerActionResult[]> = this.resultSubject.asObservable().pipe(
    scan((acc, cur) => {
      acc.unshift(cur);
      return acc;
    }, [] as PlayerActionResult[]),
    shareReplay(1)
  );

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
    this.addListeners();
  }

  private startConnection() {
    try {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl("https://localhost:7236" + "/hub/Dashboard", this.options)
        .withAutomaticReconnect()
        .build();
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
