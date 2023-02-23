import { Component, Input, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { MojConfig } from 'src/app/moj-config';
declare function porukaSuccess(a: string):any;

@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css'],
})
export class StudentEditComponent implements OnInit {
  @Input() student: any = {};
  opstinePodaci:any
  constructor(private httpKlijent: HttpClient) {}

  ngOnInit(): void {
    this.getOpstine();
  }

  Zatvori() {
    this.student.prikazi = false;
  }
  getOpstine(){
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Opstina/GetByAll", MojConfig.http_opcije()).subscribe(x=>{
      this.opstinePodaci = x;
    });
  }
  Spasi(){
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Student/Add",this.student, MojConfig.http_opcije()).subscribe(x=>{
      location.reload();
      this.student.prikazi=false;
      porukaSuccess("Uspjesno dodan/modifikovan student");
    });
  }

}
