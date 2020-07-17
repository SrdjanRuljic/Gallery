import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthService } from "./auth.services";

import { LoginComponent } from "./login/login.component";
import { LogoutComponent } from "./logout/logout.component";

@NgModule({
    imports: [
      CommonModule
    ],
    declarations: [
        LoginComponent,
        LogoutComponent   
    ],
    providers: [AuthService],
    exports: [
      LoginComponent,
      LogoutComponent
    ]
  })
  export class AuthModule {}