export interface PlayerActionResult {
    round: number;
    playerId: number;
    actionResults: ActionResult[];
}
export interface ActionResult {
    type: ActionResultType;
    result: string;
}
export enum ActionResultType {
    Action = 0,
    Success = 1,
    General = 2,
    Error = 3,
    Death = 4
}