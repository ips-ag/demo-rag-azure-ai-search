import { useState } from 'react';
import { search } from '../services';
import { Loading } from './';

export function SearchBar() {
  const [query, setQuery] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const handleSearch = async () => {
    setIsLoading(true);
    const result = await search(query);
    setIsLoading(false);
    console.log(result);
  };

  return (
    <div>
      <input type="text" placeholder="Search" value={query} onChange={(e) => setQuery(e.target.value)} />
      <button type="submit" onClick={handleSearch} disabled={isLoading}>
        Search
      </button>
      {isLoading && <Loading />}
    </div>
  );
}
