import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BiscuitBill, PumpBill, Sales, Voucher } from './addsale.model';
@Injectable({
  providedIn: 'root'
})
export class AddsaleService {
  apiUrl = environment.apiUrl;


  constructor(private http: HttpClient) { }

  addSale(sale: Sales) {
    return this.http.post(this.apiUrl + 'Sales', sale);
  }

  editSale(sale: Sales) {
    var formData = new FormData();
    formData.append("sales", JSON.stringify(sale));
    return this.http
      .post<Sales>(this.apiUrl + 'Sales', formData);
  }

  public deleteSale(id): Observable<Response> {
    return this.http
      .get<Response>(this.apiUrl + 'Sales/DeleteSale/' + id);
  }

  getSalesbyDate(date: Date) {
    return this.http.get<Sales[]>(this.apiUrl + 'Sales/GetSalesByDate/' + date.toDateString());
  }



  addVoucher(model: Voucher) {
    return this.http.post(this.apiUrl + 'Sales/VoucherBill', model);
  }

  editVoucher(model: Voucher) {
    var formData = new FormData();
    formData.append("voucher", JSON.stringify(model));
    return this.http
      .post<Voucher>(this.apiUrl + 'Sales/EditVoucher', formData);
  }

  public deleteVoucher(id): Observable<Response> {
    return this.http
      .get<Response>(this.apiUrl + 'Sales/DeleteVoucher/' + id);
  }

  getVoucherbyDate(date: Date) {
    return this.http.get<Voucher[]>(this.apiUrl + 'Sales/GetVoucherByDate/' + date.toDateString());
  }



  addPumpBill(model: PumpBill) {
    return this.http.post(this.apiUrl + 'Sales/PumpCash', model);
  }
  editPumpBill(model: PumpBill) {
    var formData = new FormData();
    formData.append("pumpbill", JSON.stringify(model));
    return this.http
      .post<PumpBill>(this.apiUrl + 'Sales/EditPumpBill', formData);
  }
  public deletePumpBill(id): Observable<Response> {
    return this.http
      .get<Response>(this.apiUrl + 'Sales/DeletePumpbill/' + id);
  }
  getPumpBillbyDate(date: Date) {
    return this.http.get<BiscuitBill[]>(this.apiUrl + 'Sales/GetPumpBillByDate/' + date.toDateString());
  }


  addBiscuitBill(model: BiscuitBill) {
    return this.http.post(this.apiUrl + 'Sales/BiscuitCash', model);
  }
  editBiscuitBill(model: BiscuitBill) {
    var formData = new FormData();
    formData.append("pumpbill", JSON.stringify(model));
    return this.http
      .post<BiscuitBill>(this.apiUrl + 'Sales/EditBiscuit', formData);
  }
  public deleteBiscuit(id): Observable<Response> {
    return this.http
      .get<Response>(this.apiUrl + 'Sales/DeleteBiscuit/' + id);
  }
  getBiscuitBillbyDate(date: Date) {
    return this.http.get<BiscuitBill[]>(this.apiUrl + 'Sales/GetBiscuitBillByDate/' + date.toDateString());
  }

}
