import { useState } from 'react';
import { search } from '../services';
import { Loading } from './Loading';
import './Search.css';

export function Search() {
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
    <>
      <div className="search-bar">
        <input
          className="search-bar__input"
          type="text"
          placeholder="Search"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
        />
        <button className="search-bar__button" type="submit" onClick={handleSearch} disabled={isLoading}>
          Search
        </button>
        {isLoading && (
          <div className="search-bar__spinner">
            <Loading />
          </div>
        )}
      </div>
      <div className="search-result">{searchResult}</div>
    </>
  );
}
