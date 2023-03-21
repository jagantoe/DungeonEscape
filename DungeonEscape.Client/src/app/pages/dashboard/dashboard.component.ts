import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardService } from 'src/app/dashboard.service';
import { ActionResultType, PlayerActionResult } from 'src/app/types/player-action-result';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent {

  results$: Observable<PlayerActionResult[]>;
  connected$: Observable<boolean>;

  ActionResultType = ActionResultType;
  constructor(private dashboardService: DashboardService) {
    this.results$ = dashboardService.results$.pipe();
    this.connected$ = dashboardService.connected$;
    this.tokenCheck();
  }

  async tokenCheck() {
    let { value: token } = await Swal.fire({
      title: 'Please enter your token',
      input: 'text',
      showCancelButton: false,
    });

    if (token) {
      this.dashboardService.connect(token);
    }
    else {
      this.tokenCheck();
    }
  }


  reconnect() {
    this.dashboardService.reconnect();
  }
}
