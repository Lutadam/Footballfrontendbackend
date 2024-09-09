import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
import { Player, PlayerWithTeam } from './models/player';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  private getplayerUrl = 'Player/GetPlayers';
  private savePlayerUrl = 'Player/Saveplayer';
  private UpdatePlayerUrl = 'Player/UpdatePlayer';
  private DeletePlayerUrl = 'Player/DeletePlayer';

  constructor(private http: HttpClient) {}

  public getPlayers(): Observable<PlayerWithTeam[]> {
    return this.http.get<PlayerWithTeam[]>(`${environment.apiUrl}/${this.getplayerUrl}`);
  }
  public createPlayers(player: Player): Observable<PlayerWithTeam[]> {
    return this.http.post<PlayerWithTeam[]>(`${environment.apiUrl}/${this.savePlayerUrl}`, 
      player);
  }
  public updatePlayers(player: Player): Observable<PlayerWithTeam[]> {
    return this.http.put<PlayerWithTeam[]>(`${environment.apiUrl}/${this.UpdatePlayerUrl}`, player);
  }
  public deletePlayers(player: Player): Observable<PlayerWithTeam[]> {
    return this.http.delete<PlayerWithTeam[]>(`${environment.apiUrl}/${this.DeletePlayerUrl}`,
      {params: { playerId: player.playerId}}
    );
  }
}
