// src/graphql/company.ts
import { gql } from '@apollo/client';

export const GET_COMPANIES = gql`
  query GetAllCompanies {
    allCompanies {
      id
      name
      zipCode
      address
      phoneNumber
      emailAddress
      homepageUrl
      establishedDate
      remarks
    }
  }
`;

export const GET_COMPANY = gql`
  query GetCompanyById($id: Int!) {
    getCompanyById(id: $id) {
      id
      name
      address
      zipCode
      phoneNumber
      emailAddress
      homepageUrl
      establishedDate
      remarks
    }
  }
`;

export const CREATE_COMPANY = gql`
  mutation CreateCompany(
    $name: String!
    $zipCode: String!
    $address: String!
    $phoneNumber: String!
    $emailAddress: String!
    $homepageUrl: String
    $establishedDate: DateTime!
    $remarks: String
  ) {
    createCompany(
      name: $name
      zipCode: $zipCode
      address: $address
      phoneNumber: $phoneNumber
      emailAddress: $emailAddress
      homepageUrl: $homepageUrl
      establishedDate: $establishedDate
      remarks: $remarks
    ) {
      id
      name
    }
  }
`;

export const UPDATE_COMPANY = gql`
  mutation UpdateCompany(
    $id: Int!
    $name: String
    $zipCode: String
    $address: String
    $phoneNumber: String
    $emailAddress: String
    $homepageUrl: String
    $establishedDate: DateTime
    $remarks: String
  ) {
    updateCompany(
      id: $id
      name: $name
      zipCode: $zipCode
      address: $address
      phoneNumber: $phoneNumber
      emailAddress: $emailAddress
      homepageUrl: $homepageUrl
      establishedDate: $establishedDate
      remarks: $remarks
    ) {
      id
      name
    }
  }
`;

export const DELETE_COMPANY = gql`
  mutation DeleteCompany($id: Int!) {
    deleteCompany(id: $id)
  }
`;
