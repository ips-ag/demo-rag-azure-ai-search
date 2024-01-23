import { useState } from 'react';
import { search } from '../services';
import { Loading } from './Loading';
import './SearchBar.css';

export function SearchBar() {
  const [query, setQuery] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [searchResult, setSearchResult] = useState('');

  const handleSearch = async () => {
    setIsLoading(true);
    const result = await search(query);
    if (result != undefined) setSearchResult(result);
    setIsLoading(false);
  };

  return (
    <div>
      <input
        className="input"
        type="text"
        placeholder="Search"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <button className="button" type="submit" onClick={handleSearch} disabled={isLoading}>
        Search
      </button>
      {isLoading && (
        <div className="searchSpinner">
          <Loading />
        </div>
      )}
      <div>
        <pre className="result">{searchResult}</pre>
      </div>
    </div>
  );
}
