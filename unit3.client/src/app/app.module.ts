import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Unit3aComponent } from './unit3a/unit3a.component';
import { NavigationMenuComponent } from './menu/nav-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    Unit3aComponent,
    NavigationMenuComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: '', component: Unit3aComponent, pathMatch: 'full' }
    ]),
  ],
  providers: [provideHttpClient(withInterceptorsFromDi())],
  bootstrap: [AppComponent]
})
export class AppModule { }
