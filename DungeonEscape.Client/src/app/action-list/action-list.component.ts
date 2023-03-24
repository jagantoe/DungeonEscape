import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { ActionResultType, PlayerActionResult } from '../types/player-action-result';

@Component({
  selector: 'action-list',
  templateUrl: './action-list.component.html',
  styleUrls: ['./action-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActionListComponent {

  @Input()
  results$!: Observable<PlayerActionResult[]>;
  ActionResultType = ActionResultType;
}
