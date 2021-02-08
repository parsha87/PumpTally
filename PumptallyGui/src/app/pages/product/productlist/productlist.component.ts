import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {
  productList: Product[] = [];

  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, private productService: ProductService, public toastr: ToastrService) { }

  ngOnInit(): void {
    this.getProductList();

  }

  deleteProduct(model:Product,i)
  {
    if (confirm("Are you sure you want to delete Product")) {
      if (model.id !== 0) {
        this.productService.deleteProduct(model.id).subscribe((response: Response) => {
          this.getProductList();
        }, error => {
          this.toastr.error('Error deleting product!', 'Information');
        });
      }
    }
    else {

    }
  }
  async getProductList(): Promise<any> {
    return new Promise<void>((resolve, reject) => {
      this.productService.GetProducts().subscribe((response: Product[]) => {
        this.productList = response;
        resolve();
      });
    });
  }

}
