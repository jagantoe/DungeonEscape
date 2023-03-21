import { Character } from "./character";

export interface Player {
    name: string;
    character: Character;
    positionX: number;
    positionY: number;
    items: string[];
    currentHealth: number;
    maxHealth: number;
    deaths: number;
    resets: number;
}