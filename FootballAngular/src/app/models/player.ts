import { Team } from './team';

export class Player {
  playerId: number;
  playerName: string;
  playerAge: number;
  playerNationality: string;
  playerShirtNumber: number;
  playerValue: number;

  constructor() {
    this.playerId = 0;
    this.playerName = '';
    this.playerAge = 0;
    this.playerNationality = '';
    this.playerShirtNumber = 0;
    this.playerValue = 0;
  }
}

export class PlayerWithTeamId extends Player {
  teamId: number;
  constructor() {
    super();
    this.teamId = 0;
  }
}

export interface PlayerWithTeam extends Player {
  team: Team;
}
