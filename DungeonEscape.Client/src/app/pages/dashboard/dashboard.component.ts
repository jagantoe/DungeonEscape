import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardService } from 'src/app/dashboard.service';
import { PlayerActionResult } from 'src/app/types/player-action-result';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  token!: string;

  results$: Observable<PlayerActionResult[]>;
  constructor(private dashboardService: DashboardService) {
    this.results$ = dashboardService.results$;
    this.tokenCheck();
    this.results$.subscribe(x => {
      console.log(x);
      console.log(x[x.length - 1]);
    }
    )
  }

  async tokenCheck() {
    let { value: token } = await Swal.fire({
      title: 'Please enter your token',
      input: 'text',
      showCancelButton: false,
    });

    if (token) {
      this.token = token;
      this.dashboardService.connect(token);
    }
    else {
      this.tokenCheck();
    }
  }

  connect() {

  }
  actions = [1, 2, 3, 4, 5, 6, 78, 8, 9, 0, 0, 0, 87, 123, 123, 6]
}
