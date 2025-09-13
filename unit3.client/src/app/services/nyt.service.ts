import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Article } from '../shared/article';

@Injectable({
  providedIn: 'root'
})
export class NewYorkTimesService {

  constructor(private http: HttpClient) {
  }

  getArticles(): Observable<Article> {
    return this.http.get<Article>('https://api.nytimes.com/svc/search/v2/articlesearch.json?&page=0&api-key=YZ6Of5u2Qn0qVFuXsAdTOh669oVkm1Dz');
  }

}
