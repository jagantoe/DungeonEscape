import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { DragToSelectModule } from 'ngx-drag-to-select';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoadingComponent } from './loading/loading.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { GameComponent } from './pages/game/game.component';
import { ActionListComponent } from './action-list/action-list.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    GameComponent,
    LoadingComponent,
    ActionListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DragToSelectModule.forRoot({
      shortcuts: {
        moveRangeStart: 'alt+meta,d',
        disableSelection: 'alt+meta,d',
        addToSelection: 'alt+meta,d',
        toggleSingleItem: 'alt+meta,d',
        removeFromSelection: 'alt+meta,d'
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
