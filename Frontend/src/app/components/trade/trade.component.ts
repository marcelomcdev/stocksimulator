import { ModalConfig } from './../../interfaces/ModalConfig';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from './../../services/signal-r.service';
import { TradeService } from './../../services/trade.service';
import { Trade } from './../../model/trade';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { BuyTradeComponent } from '../buy-trade/buy-trade.component';
import { ModalComponent } from 'src/app/shared/modal/modal.component';

@Component({
  selector: 'app-trade',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.scss']
})
export class TradeComponent implements OnInit {

  public trades: Trade[];
  closeResult: string;

  public currentTrade: Trade;
  public quantity: any;
  // public modalConfig: ModalConfig;

  constructor(private tradeService: TradeService, public signalRService: SignalRService, private http: HttpClient,  private modalService: NgbModal) { }

  ngOnInit() {

    this.quantity = 0;
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListenerToTrade();
    this.startHttpRequest();

    // this.modalConfig.modalTitle = 'Comprar Ativo'
    // this.modalConfig.closeButtonLabel = 'Salvar'
    // this.modalConfig.dismissButtonLabel = 'Sair'
    // this.modalConfig.shouldClose();
    // this.modalConfig.shouldDismiss();



  }

  private startHttpRequest = () => {
  this.http.get('https://localhost:5001/api/chart')
    .subscribe(res => {
       console.log(res);
    })
  }



  calculate(total, quantity)
  {
    return (total * quantity).toFixed(2);
  }


  open(content, current: Trade) {

    this.quantity = 0;
    this.currentTrade = current;

    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }


  // @ViewChild('modalBuyTrade') private modalComponent: ModalComponent;

  // async openModal(current: Trade){
  //   this.currentTrade = current;
  //   this.modalComponent.modalConfig = this.modalConfig;
  //   return await this.modalComponent.open();
  // }


  // openModal(current: Trade){
  //   this.quantity = 0;
  //   this.currentTrade = current;

  //   const modalRef = this.modalService.open(this.buyTradeComponent);
  //   modalRef.componentInstance.current = this.currentTrade;
  // }


}
