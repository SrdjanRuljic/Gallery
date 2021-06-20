/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AboutAuthorService } from './about-author.service';

describe('Service: AboutAuthor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AboutAuthorService]
    });
  });

  it('should ...', inject([AboutAuthorService], (service: AboutAuthorService) => {
    expect(service).toBeTruthy();
  }));
});
