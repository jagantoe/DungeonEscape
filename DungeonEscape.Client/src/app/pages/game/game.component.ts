import { ChangeDetectionStrategy, Component, HostListener } from '@angular/core';
import { debounceTime, distinctUntilChanged, firstValueFrom, map, Observable, Subject, tap } from 'rxjs';
import { GameService } from 'src/app/game.service';
import { Character } from 'src/app/types/character';
import { DisplayTile } from 'src/app/types/display-tile';
import { GameState } from 'src/app/types/gamestate';
import { Inspection } from 'src/app/types/inspection';
import { Player } from 'src/app/types/player';
import { TileKind } from 'src/app/types/tilekind';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameComponent {

  Character = Character;


  player$: Observable<Player>;
  vision$: Observable<DisplayTile[]>;
  inspection$: Observable<Inspection>;
  connected$: Observable<boolean>;
  constructor(private gameService: GameService) {
    this.player$ = gameService.player$;
    this.vision$ = gameService.gameState$.pipe(
      distinctUntilChanged((a, b) => JSON.stringify(a) == JSON.stringify(b)),
      map(x => this.mapDisplayTiles(x)),
    );
    this.inspection$ = gameService.inspection$;
    this.connected$ = gameService.connected$;
    this.tokenCheck();
  }

  private mapDisplayTiles(gameState: GameState) {
    let tiles = [];
    let player = gameState.player;
    let vision = gameState.vision;
    for (let i = -3; i < 4; i++) {
      for (let j = -3; j < 4; j++) {
        let x = j + player.positionX;
        let y = i + player.positionY;
        let tile = vision.find(tile => tile.positionX == x && tile.positionY == y);
        if (tile) {
          // +4 because of 1 based grid system (1,1 is first tile)
          tiles.push({ x: x, y: y, col: "col-start-" + (j + 4), row: "row-start-" + (i + 4), color: this.getColor(tile.tileKind) });
        }
      }
    }
    return tiles;
  }
  private getColor(kind: TileKind) {
    switch (kind) {
      case TileKind.Walkable:
        return "bg-gray-400";
      case TileKind.PointOfInterest:
        return "bg-green-500";
      default:
        return "bg-gray-800"
    }
  }

  async tokenCheck() {
    let { value: token } = await Swal.fire({
      title: 'Please enter your token',
      input: 'text',
      showCancelButton: false,
    });

    if (token) {
      this.gameService.connect(token);
    }
    else {
      this.tokenCheck();
    }
  }

  reconnect() {
    this.gameService.reconnect();
  }

  selectedTiles: DisplayTile[] = [];
  async move(x: number, y: number) {
    let player = await firstValueFrom(this.player$)
    this.gameService.move(player.positionX + x, player.positionY + y);
  }
  inspect() {
    if (this.selectedTiles.length == 0) return;
    let tile: any = this.selectedTiles[0];
    this.gameService.inspect(tile.x, tile.y)
  }
  interact() {
    if (this.selectedTiles.length == 0) return;
    let tile: any = this.selectedTiles[0];
    this.gameService.interact(tile.x, tile.y)
  }
  switchCharacter(character: Character) {
    this.gameService.switchCharacter(character);
  }
  private handle(code: string) {
    console.log(code);
    try {
      switch (code) {
        case "ArrowUp":
          this.move(0, -1);
          break;
        case "ArrowDown":
          this.move(0, 1);
          break;
        case "ArrowRight":
          this.move(1, 0);
          break;
        case "ArrowLeft":
          this.move(-1, 0);
          break;
        case "KeyF":
          this.inspect();
          break;
        case "KeyJ":
          this.interact();
          break;
        default:
          break;
      }
    } catch (error) {

    }
  }

  private actionDebouncer = new Subject<string>()
  actionDebouncer$ = this.actionDebouncer.asObservable().pipe(
    debounceTime(200),
    tap(x => this.handle(x))
  );
  // Keybinds
  @HostListener('document:keydown', ['$event'])
  private keyBinds(btn: any) {
    this.actionDebouncer.next(btn.code);
  }
}
