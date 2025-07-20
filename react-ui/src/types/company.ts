// types/company.ts

export interface Company {
  id?: number;
  name: string;
  zipCode: string;
  address: string;
  phoneNumber: string;
  emailAddress: string;
  homepageUrl?: string;
  establishedDate: string; // or Date, depending on how you handle dates
  remarks?: string;
}
