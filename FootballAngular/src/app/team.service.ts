import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Team } from './models/team';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  private getTeamUrl = 'Teams/GetTeams';
  private saveTeamUrl = 'Teams/SaveTeam';
  private UpdateTeamUrl = 'Teams/UpdateTeam';
  private DeleteTeamUrl = 'Teams/DeleteTeam';

  constructor(private http: HttpClient) {}

  public getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${environment.apiUrl}/${this.getTeamUrl}`);
  }

  public createTeam(team: Team): Observable<Team[]> {
    return this.http.post<Team[]>(
      `${environment.apiUrl}/${this.saveTeamUrl}`,
      team
    );
  }

  public updateTeam(team: Team): Observable<Team[]> {
    return this.http.put<Team[]>(
      `${environment.apiUrl}/${this.UpdateTeamUrl}`,
      team
    );
  }

  public deleteTeam(team: Team): Observable<Team[]> {
    return this.http.delete<Team[]>(
      `${environment.apiUrl}/${this.DeleteTeamUrl}`,
      { params: { teamId: team.teamId } }
    );
  }
}
