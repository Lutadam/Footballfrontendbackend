import { Routes } from '@angular/router';
import {TeamComponent}from './team/team.component';
import { HomeComponent } from './home/home.component';
import { PlayerComponent } from './player/player.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'Team',
    component: TeamComponent,
  },
  {
    path: 'Player',
    component: PlayerComponent,
  },
];
