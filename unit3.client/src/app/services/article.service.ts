import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ArticleForDb } from '../shared/article4db';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) {
  }

  getArticles(): Observable<ArticleForDb[]> {
    return this.http.get<ArticleForDb[]>('https://localhost:7173/articleservices/article');
  }


  putArticles(article: ArticleForDb): void {
    this.http.put('https://localhost:7173/articleservices/article', JSON.stringify(article)).subscribe();
  }


  postArticles(articles: ArticleForDb[]): void {
    this.http.post('https://localhost:7173/articleservices/article?json=' + JSON.stringify(articles), '').subscribe();
  }


  deleteArticles(id: number): void {
    this.http.delete('https://localhost:7173/articleservices/article?id=' + id).subscribe();
  }

}
