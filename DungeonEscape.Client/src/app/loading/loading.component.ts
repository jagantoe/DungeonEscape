import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { map, Observable, timer } from 'rxjs';

@Component({
  selector: 'loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingComponent {

  @Input()
  connected$!: Observable<boolean>;
  @Output()
  reconnect = new EventEmitter();

  reconnectTimer$: Observable<boolean> = timer(10000).pipe(map(x => true));

  connect() {
    this.reconnectTimer$ = timer(10000).pipe(map(x => true));
    this.reconnect.next(null);
  }
}
