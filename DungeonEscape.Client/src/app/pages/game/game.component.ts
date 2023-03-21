import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { GameService } from 'src/app/game.service';
import { Inspection } from 'src/app/types/inspection';
import { Player } from 'src/app/types/player';
import { Tile } from 'src/app/types/tile';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameComponent {

  token!: string;

  player$: Observable<Player>;
  vision$: Observable<Tile[]>;
  inspection$: Observable<Inspection>;
  connected$: Observable<boolean>;
  constructor(private gameService: GameService) {
    this.player$ = gameService.player$;
    this.vision$ = gameService.vision$;
    this.inspection$ = gameService.inspection$;
    this.connected$ = gameService.connected$;
    this.tokenCheck();
  }

  async tokenCheck() {
    let { value: token } = await Swal.fire({
      title: 'Please enter your token',
      input: 'text',
      showCancelButton: false,
    });

    if (token) {
      this.token = token;
      this.gameService.connect(token);
    }
    else {
      this.tokenCheck();
    }
  }

  reconnect() {
    this.gameService.reconnect();
  }
}
