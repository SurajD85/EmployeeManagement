// import { StrictMode } from 'react'
// import { createRoot } from 'react-dom/client'
// import './index.css'
// import App from './App.tsx'

// createRoot(document.getElementById('root')!).render(
//   <StrictMode>
//     <App />
//   </StrictMode>,
// )


import ReactDOM from 'react-dom/client';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import { ColorModeProvider } from './theme/ColorModeContext';
import { ApolloProvider } from '@apollo/client';
import client from './lib/apollo';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <BrowserRouter>
  <ApolloProvider client={client}>
    <ColorModeProvider>
      <App />
    </ColorModeProvider>
    </ApolloProvider>
  </BrowserRouter>
);
