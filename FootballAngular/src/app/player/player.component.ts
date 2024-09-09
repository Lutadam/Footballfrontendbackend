import { Component, ElementRef, ViewChild } from '@angular/core';
import { PlayerService } from '../player.service';
import { Player, PlayerWithTeam } from '../models/player';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TeamService } from '../team.service';
import { Team } from '../models/team';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-player',
  standalone: true,
  imports: [NgFor, RouterLink, NgIf, FormsModule],
  templateUrl: './player.component.html',
  styleUrl: './player.component.scss',
})
export class PlayerComponent {
  @ViewChild('myModal') Modal: ElementRef | undefined;
  Playerobj:Player = new Player();
  constructor(
    private _playerService: PlayerService,
    private _teamService: TeamService
  ) {}

  players: PlayerWithTeam[] = [];
  teams: Team[] = [];
  isEditMode: boolean = false;

  ngOnInit() {
    this._playerService.getPlayers().subscribe((players) => {
      this.players = players;
    });
    this._teamService.getTeams().subscribe((teams) => {
      this.teams = teams;
    });
  }

  openModal() {
    const PlayerModel = document.getElementById('myModal');
    if (PlayerModel != null) {
      PlayerModel.style.display = 'block';
    }
  }

  closeModal() {
    if (this.Modal != null) {
      this.Modal.nativeElement.style.display = 'none';
    }
  }
  onSaveform() {
    if (this.isEditMode) {
      // Here we are in edit mode, so calling update team service method
      this._playerService.updatePlayers(this.Playerobj).subscribe((players) => {
        console.log('Data has been saved successfully.');
        this.closeModal();
        this.players = players;
      });
    } else {
      // Here we are in create mode, so calling save team service method
      this._playerService.createPlayers(this.Playerobj).subscribe((players) => {
        console.log('Data has been saved successfully.');
        this.closeModal();
        this.players = players;
      });
    }
  }

  onEditPlayer(player: Player) {
    this.Playerobj = player;
    this.isEditMode = true;
    this.openModal();
  }

  onDeletePlayer(player: Player) {
    const isConfirm = confirm(
      `Do you want to delete player ${player.playerName} with id ${player.playerId}?`
    );
    if (isConfirm) {
      // If user confirms deletion, we proceed with using the delete team service method
      this._playerService.deletePlayers(player).subscribe((response) => {
        console.log(response);
        window.location.reload()
      });
    }
  }
}
