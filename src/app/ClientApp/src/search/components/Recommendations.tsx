import './Recommendations.css';

interface RecommendationsProps {
  phrases: string[];
  onPhraseSelect: (phrase: string) => void;
}

export function Recommendations(props: RecommendationsProps) {
  return (
    <div className="recommendations">
      <span className="recommendations-label">Or select one of the popular topics</span>
      {props.phrases.map((phrase, index) => (
        <button
          key={index}
          className="recommendations-item"
          onClick={() => props.onPhraseSelect(phrase)}>
          {phrase}
        </button>
      ))}
    </div>
  );
}
