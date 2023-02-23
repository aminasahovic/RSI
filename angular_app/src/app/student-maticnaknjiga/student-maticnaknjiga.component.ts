import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  student:any={}
  studentId:any
  akademskeGodine:any
  sveIzMaticne:any=[]
  ovjeri:any={}
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}

  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }

  ovjeriZimski(s:any) {

  }
  
  ngOnInit(): void {
    this.studentId=this.route.snapshot.paramMap.get("id");
    console.log(this.studentId);
    this.getStudent();
    this.getAkademske();
    this.getSveIzMaticne();
    this.ovjeri.datumOvjere=new Date().toISOString().slice(0,10)
  }

  getStudent(){
    //GetById
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetById/"+this.studentId, MojConfig.http_opcije()).subscribe(x=>{
      this.student=x;
    });
  }

  otvoriDodaj(){
    this.student.prikazi=true;
  }
  zatvori(){
    this.student.prikazi=false;
  }

  getAkademske(){
    this.httpKlijent.get(MojConfig.adresa_servera+ "/AkademskeGodine/GetAll_ForCmb", MojConfig.http_opcije()).subscribe(x=>{
      this.akademskeGodine=x;
    });
  }

  dodajNoviSemestar(){
    this.student.studentId=this.studentId;
    this.httpKlijent.post(MojConfig.adresa_servera+ "/MaticnaKnjiga/Add",this.student, MojConfig.http_opcije()).subscribe(x=>{
     
      this.student.prikazi=false;
      location.reload();
      
      porukaSuccess("Uspjeno upisan semestar");

    });
  }
  getSveIzMaticne(){
    this.httpKlijent.get(MojConfig.adresa_servera+ "/MaticnaKnjiga/GetById/"+this.studentId, MojConfig.http_opcije()).subscribe(x=>{
      this.sveIzMaticne=x;
    });
  }

  otvoriOvjeru(s:any){
    this.ovjeri.id=s.id;
    this.ovjeri.prikazi=true;
  }
  zatvoriOvjeru(){
    this.ovjeri.prikazi=false;
  }
  izvrsiOvjeru(){
    this.httpKlijent.post(MojConfig.adresa_servera+ "/MaticnaKnjiga/Ovjeri",this.ovjeri, MojConfig.http_opcije()).subscribe(x=>{
      location.reload();
      this.ovjeri.prikazi=false;
      porukaSuccess("Uspjesna ovjera");
      
    });
  }
}
