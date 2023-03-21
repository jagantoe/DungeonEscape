import { TileKind } from "./tilekind";

export interface Tile {
    positionX: number;
    positionY: number;
    tileKind: TileKind;
}