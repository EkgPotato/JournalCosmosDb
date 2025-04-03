import type { EntryViewModel } from "@/models/viewModels/EntryViewModel";
import type { EntryDTO } from "@/models/dtos/EntryDTO";
import axios from "axios";
import { NotificationService } from "@/services/NotificationService";
import type { GridRequest } from "@/models/common/requests/GridRequest";
import type { PagedResponse } from "@/models/common/PagedResponse";

export class EntryService {
    constructor(private readonly _notification: NotificationService) { }

    private readonly controller: string = "/entries";

    async getEntries(request: GridRequest): Promise<PagedResponse<EntryViewModel>> {
        let result: PagedResponse<EntryViewModel> = { items: [], totalItemsCount: 0 }
        await axios.get<PagedResponse<EntryViewModel>>(this.controller, { params: request })
            .then(response => {
                result = response.data
            })
            .catch(error => {
                this._notification?.error(`Error while retrieving data: ${error}`);
            });

        return result
    }

    async getEntry(id: string): Promise<EntryDTO | undefined> {
        let entry;

        await axios.get<EntryDTO>(`${this.controller}\\${id}`)
            .then(response => {
                entry = response.data
            })
            .catch(error => {
                this._notification?.error(`Error while retrieving data: ${error}`);
                entry = undefined;
            });

        return entry;
    }

    async updateEntry(id: string, entry: EntryDTO) {
        await axios.put(`${this.controller}\\${id}`, entry)
            .then(() => {
                this._notification?.success("Entry updated sucessfuly.");
            })
            .catch(error => {
                this._notification?.error(`There was a problem updating the item: ${error}`);
            });
    }

    async createEntry(entry: EntryDTO) {
        await axios.post(`${this.controller}`, entry)
            .then(() => {
                this._notification?.success("Entry created sucessfuly.");
            })
            .catch(error => {
                this._notification?.error(`There was a while creating the item: ${error}`);
            });
    }

    async deleteEntry(id: string) {
        await axios.delete(`${this.controller}\\${id}`)
            .then(() => {
                this._notification?.success("Entry deleted sucessfuly.");
            })
            .catch(error => {
                this._notification?.error(`There was a while deleting the item: ${error}`);
            });
    }
}
//     {
//         id: '1',
//         title: 'Sample Title 1',
//         content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-01'),
//         updatedAt: new Date('2023-01-05')
//     },
//     {
//         id: '2',
//         title: 'Sample Title 2',
//         content: 'Nunc vulputate libero et velit interdum, ac aliquet odio mattis.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-02')
//     },
//     {
//         id: '3',
//         title: 'Sample Title 3',
//         content: 'Curabitur tempus quam at diam fermentum, vitae pellentesque nulla lobortis.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-03'),
//         updatedAt: new Date('2023-01-07')
//     },
//     {
//         id: '4',
//         title: 'Sample Title 4',
//         content: 'Etiam non tellus consequat, volutpat leo ut, ultricies mauris.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-04')
//     },
//     {
//         id: '5',
//         title: 'Sample Title 5',
//         content: 'Suspendisse eget ligula efficitur, faucibus orci quis, maximus urna.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-05')
//     },
//     {
//         id: '6',
//         title: 'Sample Title 6',
//         content: 'Vivamus imperdiet ligula id tincidunt rutrum.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-06'),
//         updatedAt: new Date('2023-01-09')
//     },
//     {
//         id: '7',
//         title: 'Sample Title 7',
//         content: 'Aliquam elementum sapien quis dignissim bibendum.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-07')
//     },
//     {
//         id: '8',
//         title: 'Sample Title 8',
//         content: 'Proin vehicula ligula non neque tincidunt vestibulum. Proin vehicula ligula non neque tincidunt vestibulum. Proin vehicula ligula non neque tincidunt vestibulum. \n Proin vehicula ligula non neque tincidunt vestibulum.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-08')
//     },
//     {
//         id: '9',
//         title: 'Sample Title 9',
//         content: 'Mauris nec urna et augue fringilla aliquam.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-09'),
//         updatedAt: new Date('2023-01-11')
//     },
//     {
//         id: '10',
//         title: 'Sample Title 10',
//         content: 'Nullam vitae velit nec magna varius suscipit.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-10')
//     },
//     {
//         id: '11',
//         title: 'Sample Title 11',
//         content: 'Cras sed tellus id nibh placerat fermentum.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-11')
//     },
//     {
//         id: '12',
//         title: 'Sample Title 12',
//         content: 'In lacinia justo sed nisl pretium, vitae blandit nunc convallis.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-12'),
//         updatedAt: new Date('2023-01-14')
//     },
//     {
//         id: '13',
//         title: 'Sample Title 13',
//         content: 'Aenean nec metus nec lorem ultrices bibendum.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-13')
//     },
//     {
//         id: '14',
//         title: 'Sample Title 14',
//         content: 'Donec nisi metus, elementum id elit sit amet, tempor euismod sapien.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-14')
//     },
//     {
//         id: '15',
//         title: 'Sample Title 15',
//         content: 'Quisque consequat lacus sit amet est elementum feugiat.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-15')
//     },
//     {
//         id: '16',
//         title: 'Sample Title 16',
//         content: 'Vestibulum faucibus ex vel magna efficitur, eget dictum leo tincidunt.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-16'),
//         updatedAt: new Date('2023-01-19')
//     },
//     {
//         id: '17',
//         title: 'Sample Title 17',
//         content: 'Sed posuere dui ut nibh viverra gravida.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-17')
//     },
//     {
//         id: '18',
//         title: 'Sample Title 18',
//         content: 'Pellentesque bibendum arcu vitae sapien laoreet cursus.',
//         category: 'Category A',
//         createdAt: new Date('2023-01-18')
//     },
//     {
//         id: '19',
//         title: 'Sample Title 19',
//         content: 'Integer finibus quam vel dolor scelerisque efficitur.',
//         category: 'Category B',
//         createdAt: new Date('2023-01-19')
//     },
//     {
//         id: '20',
//         title: 'Sample Title 20',
//         content: 'Praesent eget nunc semper, facilisis enim eu, accumsan nibh.',
//         category: 'Category C',
//         createdAt: new Date('2023-01-20'),
//         updatedAt: new Date('2023-01-22')
//     }
// ];
