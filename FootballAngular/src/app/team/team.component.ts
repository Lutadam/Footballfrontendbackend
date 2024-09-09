import { Component, ElementRef, ViewChild } from '@angular/core';
import { TeamService } from '../team.service';
import { Team } from '../models/team';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-team',
  standalone: true,
  imports: [NgFor, RouterLink, NgIf, FormsModule],
  templateUrl: './team.component.html',
  styleUrl: './team.component.scss',
})
export class TeamComponent {
  @ViewChild('myModal') Modal: ElementRef | undefined;
  Teamobj: Team = new Team();
  constructor(private _teamService: TeamService) {}

  teams: Team[] = [];
  isEditMode: boolean = false;

  ngOnInit() {
    this._teamService.getTeams().subscribe((teams) => {
      this.teams = teams;
    });
  }

  openModal() {
    const TeamModel = document.getElementById('myModal');
    if (TeamModel != null) {
      TeamModel.style.display = 'block';
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
      this._teamService.updateTeam(this.Teamobj).subscribe((teams) => {
        console.log('Data has been saved successfully.');
        this.closeModal();
        this.teams = teams;
      });
    } else {
      // Here we are in create mode, so calling save team service method
      this._teamService.createTeam(this.Teamobj).subscribe((teams) => {
        console.log('Data has been saved successfully.');
        this.closeModal();
        this.teams = teams;
      });
    }
  }

  onEditTeam(team: Team) {
    this.Teamobj = team;
    this.isEditMode = true;
    this.openModal();
  }

  onDeleteTeam(team: Team) {
    const isConfirm = confirm(
      `Do you want to delete team ${team.teamName} with id ${team.teamId}?`
    );
    if (isConfirm) {
      // If user confirms deletion, we proceed with using the delete team service method
      this._teamService.deleteTeam(team).subscribe((response) => {
        console.log(response);
        window.location.reload()
      });
    }
  }
}
