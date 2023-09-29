import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../Models/user';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Contact } from '../../Models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiurl = "https://localhost:7267/api/Contact";


  constructor(private http: HttpClient) { }

  public SendContactForm(contact: Contact): Observable<any> {
    return this.http.post(`${this.apiurl}/`, contact);
  }

}
