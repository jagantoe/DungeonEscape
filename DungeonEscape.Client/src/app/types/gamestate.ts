import { Player } from "./player";
import { Tile } from "./tile";

export interface GameState {
    player: Player;
    visionTiles: Tile[];
}