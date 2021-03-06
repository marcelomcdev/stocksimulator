import { RegisterUserComponent } from './components/register-user/register-user.component';
import { ModalFormService } from './services/modal-service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ImageLogoComponent } from './shared/image-logo/image-logo.component';
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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { ChartsModule } from 'ng2-charts';
import { ModalComponent } from './shared/modal/modal.component';
import { BuyTradeComponent } from './components/buy-trade/buy-trade.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    NavMenuComponent,
    TradeComponent,
    ModalComponent,
    BuyTradeComponent,
    RegisterUserComponent,
   ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    AppRoutingModule,
    HttpClientModule,
    ChartsModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'entrar', component: LoginComponent },
      //{ path: 'acoes', component: TradeComponent },
      { path: 'acoes', component: TradeComponent, canActivate: [GuardaRotas] },
      { path: 'novo-usuario', component: RegisterUserComponent }
    ])
  ],
  providers: [UserService, TradeService, ModalFormService],
  bootstrap: [AppComponent],
  entryComponents: [ BuyTradeComponent ]
})
export class AppModule { }
