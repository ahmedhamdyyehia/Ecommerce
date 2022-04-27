import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';
// import { error } from 'console';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Ecommerce';

  Products:IProduct[];
  constructor(private http:HttpClient)
  {

  }
  ngOnInit(): void {
    this.http.get('https://localhost:7225/api/products?PageSize=50').subscribe((Response:IPagination)=> {
      this.Products=Response.data;
      console.log(Response);
    },error=>{
      console.log(error);
    });

  }
}
