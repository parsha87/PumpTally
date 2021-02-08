import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Sales } from '../addsale/addsale.model';
import { Product } from './product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addSaleProduct(product: Product) {
    return this.http.post(this.apiUrl + 'Product', product);
  }

  //Get Sale by ID
  getProductbyId(id: string) {
    return this.http.get<Product>(this.apiUrl + 'Product/' + id);
  }

  GetProducts() {
    return this.http.get<Product[]>(this.apiUrl + 'Product');
  }

  public deleteProduct(id): Observable<Response> {
    return this.http
      .get<Response>(this.apiUrl + 'Product/DeleteProduct/' + id);
  }
}
