// import { TestBed } from '@angular/core/testing';
// import { HttpClient } from '@angular/common/http';
// import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
// import { TestBed } from '@angular/core/testing';
// import { HttpClient } from '@angular/common/http';
// import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';

// import { ApiService } from './api.service';
// import { environment } from 'src/environments/environment';
// import { ApiService } from './api.service';
// import { environment } from 'src/environments/environment';

// describe('ApiService', () => {
//   let httpClient: HttpClient;
//   let httpTestingController: HttpTestingController;
//   let service: ApiService;
// describe('ApiService', () => {
//   let httpClient: HttpClient;
//   let httpTestingController: HttpTestingController;
//   let service: ApiService;

//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule],
//     });
//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule],
//     });

//     httpClient = TestBed.inject(HttpClient);
//     httpTestingController = TestBed.inject(HttpTestingController);
//     service = TestBed.inject(ApiService);
//   });
//     httpClient = TestBed.inject(HttpClient);
//     httpTestingController = TestBed.inject(HttpTestingController);
//     service = TestBed.inject(ApiService);
//   });

//   afterEach(() => {
//     httpTestingController.verify();
//   })
//   afterEach(() => {
//     httpTestingController.verify();
//   })

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });

//   it('should make an API call for TreeSpecies', () => {
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Mangrove Tree',
//         description: 'A tree originating from swampy areas',
//         picturePath: '',
//       },
//     ];
//   it('should make an API call for TreeSpecies', () => {
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Mangrove Tree',
//         description: 'A tree originating from swampy areas',
//         picturePath: '',
//       },
//     ];

//     service.getAllTrees().subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });
//     service.getAllTrees().subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });

//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/TreeSpecies'
//     );
//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/TreeSpecies'
//     );

//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });

//   it('should make an API call for TreeSpeciesDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Mangrove Tree',
//         description: 'A tree originating from swampy areas',
//         picturePath: '',
//       },
//     ];
//   it('should make an API call for TreeSpeciesDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Mangrove Tree',
//         description: 'A tree originating from swampy areas',
//         picturePath: '',
//       },
//     ];

//     service.getTreeDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });
//     service.getTreeDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });

//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/TreeSpecies/' + id
//     );

//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });

//   it('should make an API call for Zones', () => {
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Zone 1',
//         description: 'The first zone'
//       },
//     ];

//     service.getAllZones().subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });
//     service.getAllZones().subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });

//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/Zone'
//     );
//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/Zone'
//     );

//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });
//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });

//   it('should make an API call for ZoneDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Zone 1',
//         description: 'The first zone'
//       },
//     ];
//   it('should make an API call for ZoneDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Zone 1',
//         description: 'The first zone'
//       },
//     ];

//     service.getZoneDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });
//     service.getZoneDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });

//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/Zone/' + id
//     );
//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/Zone/' + id
//     );

//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });
//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });

//   // it('should make an API call for Tasks', () => {
//   //   const mockResponse = [
//   //     {
//   //       id: 1,
//   //       name: 'Task 1',
//   //       description: 'Do this',
//   //       workschedule: null,
//   //       total_pages: 1,
//   //       total_results: 1
//   //     },
//   //   ];

//   //   service.getAllTasks().subscribe((res) => {
//   //     expect(res).toBeTruthy();
//   //     expect(res).toHaveSize(1);
//   //     const treeSpecies = res[0];
//   //     expect(treeSpecies).toBe(mockResponse[0]);
//   //   });

//   //   const mockRequest = httpTestingController.expectOne(
//   //     environment.baseUrl + '/EmployeeTask'
//   //   );

//   //   expect(mockRequest.request.method).toEqual('GET');
//   //   mockRequest.flush(mockResponse);
//   // });

//   it('should make an API call for TaskDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Task 1',
//         description: 'Do this',
//         workschedule: null,
//         total_pages: 1,
//         total_results: 1
//       },
//     ];
//   it('should make an API call for TaskDetail', () => {
//     const id = "1";
//     const mockResponse = [
//       {
//         id: 1,
//         name: 'Task 1',
//         description: 'Do this',
//         workschedule: null,
//         total_pages: 1,
//         total_results: 1
//       },
//     ];

//     service.getTaskDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });
//     service.getTaskDetails(id).subscribe((res) => {
//       expect(res).toBeTruthy();
//       expect(res).toHaveSize(1);
//       const treeSpecies = res[0];
//       expect(treeSpecies).toBe(mockResponse[0]);
//     });

//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/EmployeeTask/' + id
//     );
//     const mockRequest = httpTestingController.expectOne(
//       environment.baseUrl + '/EmployeeTask/' + id
//     );

//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });
// });
//     expect(mockRequest.request.method).toEqual('GET');
//     mockRequest.flush(mockResponse);
//   });
// });
