import { useEffect, useState } from 'react';
import { search } from '../services';
import { Loading } from './Loading';
import './Search.css';

interface SearchProps {
  query: string;
  onSearchResultChange: (text: string) => void;
}

export function Search(props: SearchProps) {
  const [query, setQuery] = useState(props.query);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (isLoading) return;
    console.log('Query changed to: ' + props.query);
    setQuery(props.query);
    //console.log('Triggering search');
    //handleSearch().catch(console.error);
  }, [props.query]);

  const handleSearch = async () => {
    console.log('Searching for: ' + query);
    setIsLoading(true);
    const result = await search(query);
    if (result != undefined) {
      props.onSearchResultChange(result);
    }
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
    </>
  );
}
