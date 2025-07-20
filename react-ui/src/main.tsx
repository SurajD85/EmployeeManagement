
import ReactDOM from 'react-dom/client';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import { ColorModeProvider } from './theme/ColorModeContext';
import { ApolloProvider } from '@apollo/client';
import React from 'react';
import { client } from './lib/apollo';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
      <ApolloProvider client={client}>
        <ColorModeProvider>
          <App />
        </ColorModeProvider>
      </ApolloProvider>
    </BrowserRouter>
  </React.StrictMode>
);
