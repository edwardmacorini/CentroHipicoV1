import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from "@auth0/angular-jwt";

import { AuthModule } from './modules/auth/auth.module';
import { HomeModule } from './modules/home/home.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem("access_token");
        },
        allowedDomains: ["localhost:4200"],
        disallowedRoutes: [],
      },
    }),
    /* My Modules */
    AuthModule,
    HomeModule,
    /* End My Modules */
  ],
  exports: [JwtModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
