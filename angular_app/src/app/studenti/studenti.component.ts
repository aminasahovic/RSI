import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  studentZaSlanje:any={}


  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.studentPodaci = x;
    });
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  filter(){
    let podaci=this.studentPodaci;
    if(this.filter_ime_prezime && this.ime_prezime!=null){
      podaci=podaci.filter((s:any)=> s.ime.startsWith(this.ime_prezime) || s.prezime.startsWith(this.ime_prezime))
    }
    if(this.filter_opstina && this.opstina!=null){
      podaci=podaci.filter((s:any)=> s.opstina_rodjenja.description.startsWith(this.opstina))
    }
    return podaci;
  }

  Uredi(s:any){
    this.studentZaSlanje=s;
    this.studentZaSlanje.prikazi=true;
  }
  Dodaj(){
    this.studentZaSlanje={
      id:0,
      ime:this.ime_prezime,
      prezime:"",
      opstina_rodjenja_id:10
    };
    this.studentZaSlanje.prikazi=true;
  }
  Obrisi(s:any){
    this.httpKlijent.delete(MojConfig.adresa_servera+ "/Student/Remove/"+s.id, MojConfig.http_opcije()).subscribe(x=>{

      location.reload();
      porukaSuccess("Uspjenso obrisana korsinik");
    
    });
  }
  OtvoriMaticnu(s:any){
    this.router.navigateByUrl("student-maticnaknjiga/"+s.id);
  }
}
