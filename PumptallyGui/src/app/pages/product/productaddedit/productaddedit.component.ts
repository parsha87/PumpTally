import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-productaddedit',
  templateUrl: './productaddedit.component.html',
  styleUrls: ['./productaddedit.component.css']
})
export class ProductaddeditComponent implements OnInit {

  productInfo: Product = new Product();
  isEdit: boolean = false;
  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient, private productService: ProductService, public toastr: ToastrService) { }

  ngOnInit(): void {
    var isEdit: string = this.route.snapshot.pathFromRoot[1].queryParams['isEdit'];
    var id: string = this.route.snapshot.pathFromRoot[2].queryParams['id'];
    if(+isEdit ===1)
    {
      this.isEdit =true;
    }
    else
    {
      this.isEdit = false;
    }
    if (+id !== 0) {
      this.getProductById(id)
    }


  }
  getProductById(id) {
    this.productService.getProductbyId(id).subscribe((response: Product) => {
      this.productInfo = response;
    });
  }

  addProduct(formData: NgForm) {
    if (formData.invalid) {
      return;
    }
    this.productInfo.codeNo = + this.productInfo.codeNo;
    this.productInfo.purchasePrice = + this.productInfo.purchasePrice;
    this.productInfo.salesPrice = + this.productInfo.salesPrice;
    this.productInfo.qty = + this.productInfo.qty;

    this.productService.addSaleProduct(this.productInfo).subscribe((response: Response) => {
      console.log(response);
      this.toastr.success('Saved Successfully');
    }, error => {
      this.toastr.error('Product not updated or Product already exists!', 'Information');
    });
    this.productInfo = new Product();
  }

}
