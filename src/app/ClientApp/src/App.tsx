import './App.css';
import { Recommendations, Search, SearchResult } from './search';
import { useState } from 'react';

function App() {
  const [searchResult, setSearchResult] = useState('');
  const [query, setQuery] = useState('');

  const handleSearchResultChange = (newSearchResult: string) => {
    setSearchResult(newSearchResult);
  };

  const onPhraseSelect = (phrase: string) => {
    setQuery(phrase);
  };

  return (
    <div className="app-body">
      <Search onSearchResultChange={handleSearchResultChange} query={query} />
      <Recommendations phrases={['love story', 'space adventure', 'lovecraftian horror', 'dystopian future']} onPhraseSelect={onPhraseSelect} />
      <SearchResult text={searchResult} />
    </div>
  );
}

export default App;
