import { TradeService } from './services/trade.service';
import { TradeComponent } from './components/trade/trade.component';
import { GuardaRotas } from './authorization/guarda.rotas';
import { AssetComponent } from './components/asset/asset.component';
import { LoginComponent } from './components/login/login.component';
import { UserService } from './services/user.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    NavMenuComponent,
    TradeComponent
   ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'entrar', component: LoginComponent },
      //{ path: 'acoes', component: TradeComponent },
      { path: 'acoes', component: TradeComponent, canActivate: [GuardaRotas] },
      //{ path: 'novo-usuario', component: CadastoUsuarioComponent }
    ])
  ],
  providers: [UserService, TradeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
