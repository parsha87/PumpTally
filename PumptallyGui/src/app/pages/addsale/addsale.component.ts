import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbCalendar, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../product/product.model';
import { ProductService } from '../product/product.service';
import { BiscuitBill, HSDMeter, MSMeter, PumpBill, Sales, Voucher } from './addsale.model';
import { AddsaleService } from './addsale.service';

@Component({
  selector: 'app-addsale',
  templateUrl: './addsale.component.html',
  styleUrls: ['./addsale.component.css']
})
export class AddsaleComponent implements OnInit {
  model = 1;
  showOpt1: boolean = true;
  showOpt2: boolean = false;
  showOpt3: boolean = false;
  constructor(private datePipe: DatePipe, private http: HttpClient, private productService: ProductService, private salesservice: AddsaleService, public toastr: ToastrService, private ngbCalendar: NgbCalendar, private dateAdapter: NgbDateAdapter<string>) { }
  selectedDate: string;
  selectedShift: number;

  hSDMeter:HSDMeter = new HSDMeter();
  msMeter:MSMeter= new MSMeter();

  productList: Product[] = [];


  saleInfo: Sales = new Sales();
  salesList: Sales[] = [];

  voucherInfo: Voucher = new Voucher();
  voucherList: Voucher[] = [];

  pumpInfo: PumpBill = new PumpBill();
  pumpList: PumpBill[] = [];

  biscuitInfo: BiscuitBill = new BiscuitBill();
  biscuitList: BiscuitBill[] = [];

  ngOnInit(): void {
    var dateObj = new Date();
    this.selectedShift = 1;
    this.saleInfo.shift = 1
    this.selectedDate = dateObj.getDate() + '/' + dateObj.getMonth() + 1 + '/' + dateObj.getFullYear();
    this.getProductList();
    this.getSalesListByDate();
    this.getVoucherListByDate();
    this.getPumpBillListByDate();
    this.getBiscuitBillListByDate();

  }

  loadList() {
    this.getSalesListByDate();
    this.getVoucherListByDate();
    this.getPumpBillListByDate();
    this.getBiscuitBillListByDate();
  }

  getSalesListByDate() {
    this.salesservice.getSalesbyDate(new Date(this.selectedDate.split('/').reverse().join('/'))).subscribe((response: Sales[]) => {
      this.salesList = response.filter(x => x.shift === this.selectedShift);
      this.salesList.forEach(element => {
        var ss = this.productList.filter(x => x.id === element.productId)[0].productName;
        element.productName = ss;
      });
    });
  }
  getVoucherListByDate() {
    this.salesservice.getVoucherbyDate(new Date(this.selectedDate.split('/').reverse().join('/'))).subscribe((response: Voucher[]) => {
      this.voucherList = response.filter(x => x.shift === this.selectedShift);;
    });
  }
  getPumpBillListByDate() {
    this.salesservice.getPumpBillbyDate(new Date(this.selectedDate.split('/').reverse().join('/'))).subscribe((response: PumpBill[]) => {
      this.pumpList = response.filter(x => x.shift === this.selectedShift);;
    });
  }

  getBiscuitBillListByDate() {
    this.salesservice.getBiscuitBillbyDate(new Date(this.selectedDate.split('/').reverse().join('/'))).subscribe((response: BiscuitBill[]) => {
      this.biscuitList = response.filter(x => x.shift === this.selectedShift);;
    });
  }

  getProductListold() {
    this.productService.GetProducts().subscribe((response: Product[]) => {
      this.productList = response;
    });
  }

  async getProductList(): Promise<any> {
    return new Promise<void>((resolve, reject) => {
      this.productService.GetProducts().subscribe((response: Product[]) => {
        this.productList = response;
        resolve();
      });
    });
  }


  onSubmit(Addform: NgForm) {
    console.log(Addform.value);
    // var SaleInfo = {
    //   Id: AddForm.value.id,
    //   Type: AddForm.value.type
    // };
    this.salesservice.addSale(this.saleInfo).subscribe((response: Response) => {
      console.log(response);
      if (response.statusText == "Success") {
        this.toastr.success('Saved Successfully');
        //this.getNoteTypeList();
        Addform.reset();
      }
      else {
        this.toastr.error("Note Type already exists");
      }

    });
  }

  editSale(sale: Sales) {
    if (sale.id === 0) {
      this.saleInfo = sale;
    }
    else {

    }
  }

  addSale() {
    this.saleInfo.productId = +this.saleInfo.productId;
    this.saleInfo.shift = this.selectedShift;
    this.saleInfo.rate = +this.saleInfo.rate;
    this.saleInfo.qty = +this.saleInfo.qty;
    this.saleInfo.qtyPurchased = +this.saleInfo.qtyPurchased;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    this.saleInfo.dateofSale = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.saleInfo.amount = (+this.saleInfo.qty * +this.saleInfo.rate);
    if (this.salesList.some(x => x.productId === this.saleInfo.productId)) {
      alert("Product already in list. Edit the sale.");
      return;
    }
    this.salesList.push(this.saleInfo);
    this.salesservice.addSale(this.saleInfo).subscribe((response: Response) => {
      console.log(response);
      this.toastr.success('Saved Successfully');
    }, error => {
      this.toastr.error('Sale not updated!', 'Information');
    });
    this.saleInfo = new Sales();
  }

  UpdateSales(salesModel: Sales, i: number) {
    salesModel.shift = this.selectedShift;
    salesModel.rate = +salesModel.rate;
    salesModel.qty = +salesModel.qty;
    this.saleInfo.qtyPurchased = +this.saleInfo.qtyPurchased;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    salesModel.dateofSale = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    salesModel.amount = (+salesModel.qty * +salesModel.rate);
    this.salesservice.addSale(salesModel).subscribe((response: Sales) => {
      this.toastr.success("Update Successfully");
    }, error => {
      this.toastr.error('Sale not updated!', 'Information');
    });
  }

  deleteSale(model: Sales, i: number) {
    if (confirm("Are you sure you want to delete sale")) {
      if (model.id !== 0) {
        this.salesservice.deleteSale(model.id).subscribe((response: Response) => {
          this.getSalesListByDate();
        }, error => {
          this.toastr.error('Error deleting sale!', 'Information');
        });
      }
    }
    else {

    }

  }





  addVoucher() {
    this.voucherInfo.ammount = +this.voucherInfo.ammount;
    this.voucherInfo.shift = this.selectedShift;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    this.voucherInfo.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addVoucher(this.voucherInfo).subscribe((response: Response) => {
      console.log(response);
      this.getVoucherListByDate();
      this.toastr.success('Saved Successfully');
    }, error => {
      this.toastr.error('Voucher not updated!', 'Information');
    });
    this.voucherInfo = new Voucher();
  }

  UpdateVoucher(model: Voucher, i: number) {
    model.shift = this.selectedShift;
    model.ammount = +model.ammount;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    model.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addVoucher(model).subscribe((response: Voucher) => {
      this.getVoucherListByDate();
      this.toastr.success("Update Successfully");
    }, error => {
      this.toastr.error('Sale not updated!', 'Information');
    });
  }

  deleteVoucher(model: Voucher, i: number) {
    if (confirm("Are you sure you want to delete Voucher")) {
      if (model.id !== 0) {
        this.salesservice.deleteVoucher(model.id).subscribe((response: Response) => {
          this.getVoucherListByDate();
        }, error => {
          this.toastr.error('Error deleting sale!', 'Information');
        });
      }
    }
    else {

    }

  }




  addPumpbill() {
    this.pumpInfo.amount = +this.pumpInfo.amount;
    this.pumpInfo.shift = this.selectedShift;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    this.pumpInfo.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addPumpBill(this.pumpInfo).subscribe((response: Response) => {
      console.log(response);
      this.getPumpBillListByDate();
      this.toastr.success('Saved Successfully');
    }, error => {
      this.toastr.error('Pump bill not updated!', 'Information');
    });
    this.pumpInfo = new PumpBill();
  }

  UpdatePumpBill(model: PumpBill, i: number) {
    model.shift = this.selectedShift;
    model.amount = +model.amount;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    model.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addPumpBill(model).subscribe((response: PumpBill) => {
      this.getPumpBillListByDate();
      this.toastr.success("Update Successfully");
    }, error => {
      this.toastr.error('Pump Cash not updated!', 'Information');
    });
  }

  deletePumpBill(model: PumpBill, i: number) {
    if (confirm("Are you sure you want to delete Pumpbill")) {
      if (model.id !== 0) {
        this.salesservice.deletePumpBill(model.id).subscribe((response: Response) => {
          this.getPumpBillListByDate();
        }, error => {
          this.toastr.error('Error deleting sale!', 'Information');
        });
      }
    }
    else {

    }

  }



  addBiscuit() {
    this.biscuitInfo.amount = +this.biscuitInfo.amount;
    this.biscuitInfo.shift = this.selectedShift;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    this.biscuitInfo.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addBiscuitBill(this.biscuitInfo).subscribe((response: Response) => {
      console.log(response);
      this.getBiscuitBillListByDate();
      this.toastr.success('Saved Successfully');
    }, error => {
      this.toastr.error('Voucher not updated!', 'Information');
    });
    this.biscuitInfo = new BiscuitBill();
  }

  UpdateBiscuit(model: BiscuitBill, i: number) {
    model.shift = this.selectedShift;
    model.amount = +model.amount;
    var milliseconds = new Date(this.selectedDate.split('/').reverse().join('/'));
    model.date = this.datePipe.transform(milliseconds, 'MM/dd/yyyy HH:mm:ss');
    this.salesservice.addBiscuitBill(model).subscribe((response: Voucher) => {
      this.getBiscuitBillListByDate();
      this.toastr.success("Update Successfully");
    }, error => {
      this.toastr.error('Sale not updated!', 'Information');
    });
  }

  deleteBiscuit(model: BiscuitBill, i: number) {
    if (confirm("Are you sure you want to delete Biscuit")) {
      if (model.id !== 0) {
        this.salesservice.deleteBiscuit(model.id).subscribe((response: Response) => {
          this.getBiscuitBillListByDate();
        }, error => {
          this.toastr.error('Error deleting sale!', 'Information');
        });
      }
    }
    else {

    }

  }



  onChange(event: Event) {
    this.saleInfo.productName = event.target['options'][event.target['options'].selectedIndex].text;
  }

  multiplyrate(i: number) {
    this.salesList[i].amount = +this.salesList[i].qty * +this.salesList[i].rate;
  }

  onEditClick(event, index: number, eventModel: Sales) {
    eventModel.inedit = true;
    if (this.saleInfo.inedit == true) {
      this.toastr.error("Update the edited entry");
      return;
    }
  }

  onVoucherEditClick(event, index: number, eventModel: Voucher) {
    eventModel.inedit = true;
    if (this.voucherInfo.inedit == true) {
      this.toastr.error("Update the edited entry");
      return;
    }
  }

  onPumpEditClick(event, index: number, eventModel: PumpBill) {
    eventModel.inedit = true;
    if (this.pumpInfo.inedit == true) {
      this.toastr.error("Update the edited entry");
      return;
    }
  }

  onBiscuitEditClick(event, index: number, eventModel: BiscuitBill) {
    eventModel.inedit = true;
    if (this.biscuitInfo.inedit == true) {
      this.toastr.error("Update the edited entry");
      return;
    }
  }

  getSalesTotal() {
    const sum = this.salesList.filter(item => item.shift === this.selectedShift)
      .reduce((sum, current) => sum + current.amount, 0);
    return sum;
  }

  getvoucherTotal() {
    const sum = this.voucherList.filter(item => item.shift === this.selectedShift)
      .reduce((sum, current) => sum + current.ammount, 0);
    return sum;
  }

  getpumpTotal() {
    const sum = this.pumpList.filter(item => item.shift === this.selectedShift)
      .reduce((sum, current) => sum + current.amount, 0);
    return sum;
  }

  getbiscuitTotal() {
    const sum = this.biscuitList.filter(item => item.shift === this.selectedShift)
      .reduce((sum, current) => sum + current.amount, 0);
    return sum;
  }

  getGrandTotal()
  {
    const st = +this.getSalesTotal();
    const pt = +this.getpumpTotal();
    const bt = +this.getbiscuitTotal();
    const ss =st + pt+bt;
    return ss;
  }

getNetAmountGiven(){
  return +this.getGrandTotal() - +this.getvoucherTotal();
}

  public open(event, item) {
    if (item == "opt1") {
      this.showOpt1 = true;
      this.showOpt2 = false;
      this.showOpt3 = false;
      this.saleInfo.shift = 1;
      this.selectedShift = 1;
      this.loadList();
    }
    else if (item == "opt2") {
      this.showOpt2 = true;
      this.showOpt1 = false;
      this.showOpt3 = false;
      this.saleInfo.shift = 2;
      this.selectedShift = 2;
      this.loadList();

    }
    else if (item == "opt3") {
      this.showOpt2 = false;
      this.showOpt1 = false;
      this.showOpt3 = true;
      this.saleInfo.shift = 3;
      this.selectedShift = 3;
      this.loadList();


    }
  }

}
