import { ChangeDetectionStrategy, ChangeDetectorRef, Component, HostListener } from '@angular/core';
import { Observable, Subject, combineLatest, debounceTime, distinctUntilChanged, firstValueFrom, map, tap, timer } from 'rxjs';
import { DashboardService } from 'src/app/dashboard.service';
import { GameService } from 'src/app/game.service';
import { Character } from 'src/app/types/character';
import { DisplayTile } from 'src/app/types/display-tile';
import { GameState } from 'src/app/types/gamestate';
import { Inspection } from 'src/app/types/inspection';
import { Player } from 'src/app/types/player';
import { PlayerActionResult } from 'src/app/types/player-action-result';
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
  results$: Observable<PlayerActionResult[]>;
  connected$: Observable<boolean>;
  constructor(private gameService: GameService, private dashboardService: DashboardService, private cdk: ChangeDetectorRef) {
    this.player$ = gameService.player$;
    this.vision$ = gameService.gameState$.pipe(
      distinctUntilChanged((a, b) => JSON.stringify(a) == JSON.stringify(b)),
      map(x => this.mapDisplayTiles(x)),
    );
    timer(1000, 500).pipe(tap(x => this.cdk.detectChanges())).subscribe();
    this.inspection$ = gameService.inspection$;
    this.results$ = dashboardService.results$;
    this.connected$ = combineLatest([gameService.connected$, dashboardService.connected$]).pipe(
      map(([a, b]) => a || b)
    );
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
      case TileKind.Danger:
        return "bg-red-500";
      default:
        return "bg-gray-800";
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
      this.dashboardService.connect(token);
    }
    else {
      this.tokenCheck();
    }
  }

  reconnect() {
    this.gameService.reconnect();
    this.dashboardService.reconnect();
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
  reset() {
    this.gameService.reset();
  }
  private handle(code: string) {
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
        case "KeyI":
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
