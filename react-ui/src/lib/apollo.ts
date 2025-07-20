// src/lib/apollo.ts
import { ApolloClient, HttpLink, InMemoryCache } from '@apollo/client';

// const client = new ApolloClient({
//   uri: 'http://localhost:5252/graphql/', // Your GraphQL endpoint
//   cache: new InMemoryCache(),
// });

const client = new ApolloClient({
  link: new HttpLink({
    uri: 'http://localhost:5252/graphql', // Your .NET backend
  }),
  cache: new InMemoryCache(),
});

export default client;
