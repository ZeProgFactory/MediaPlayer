using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.Media
{
   class Playlist : ObservableCollection<IMediaItem>, IPlaylist
   {
      public RepeatMode RepeatMode { get; set; }

      // - - -  - - - 

      public ShuffleMode ShuffleMode
      {
         get
         {
            return _shuffleMode;
         }
         set
         {
            _shuffleMode = value;

            if (ShuffleMode == ShuffleMode.On)
            {
               // Create a shuffled remainder of the queue
               CreateShuffledIndexes();
               CollectionChanged += (s, e) => CreateShuffledIndexes();
            }
            else
            {
               CollectionChanged -= (s, e) => CreateShuffledIndexes();
            }
         }
      }

      private ShuffleMode _shuffleMode = ShuffleMode.Off;

      private int _indexOfCurrentItemInShuffledIndexes => _shuffledIndexes.Select((v, i) => new { originalIndex = v, index = i }).First(x => x.originalIndex == CurrentIndex).index;
      private IList<int> _shuffledIndexes;

      private void CreateShuffledIndexes()
      {
         Random rand = new Random();
         var ints = Enumerable.Range(CurrentIndex + 1, Count - 1)
             .Select(i => new Tuple<int, int>(rand.Next(Count), i))
             .OrderBy(i => i.Item1)
             .Select(i => i.Item2)
             .ToList();

         // We always put the current index at the start of the list
         ints.Insert(0, CurrentIndex);
         _shuffledIndexes = ints;
      }

      // - - -  - - - 

      public IMediaItem Current
      {
         get => _Current;
         set
         {
            if (_Current != value)
            {
               _Current = value;

               if (_Current != null && this.IndexOf(_Current) < 0)
               {
                  this.Add(_Current);
               };

               OnPropertyChanged(new PropertyChangedEventArgs("Current"));
            };
         }
      }

      IMediaItem _Current = null;

      //private int _currentIndex = 0;
      public int CurrentIndex
      {
         get => (_Current == null ? -1 : this.IndexOf(_Current));
         //get => _currentIndex;
         //set
         //{
         //   if (_currentIndex != value)
         //   {
         //      //ToDo: OnQueueChanged(this, new QueueChangedEventArgs());
         //   };

         //   _currentIndex = value;
         //}
      }

      // - - -  - - - 

      protected override void ClearItems()
      {
         base.ClearItems();

         Current = null;
      }


      public string Title { get; set; }


      public void AddRange(List<IMediaItem> playList)
      {
         throw new NotImplementedException();
      }

      public void PlayByPosition(int ind)
      {
         if (ind < this.Count)
         {
            MediaPlayer.Current.Play(this[ind]);
         };
      }

      // - - -  - - - 

      public bool HasNext() => ShuffleMode == ShuffleMode.On ? _shuffledIndexes.Count() > _indexOfCurrentItemInShuffledIndexes + 1 : Count > CurrentIndex + 1;

      public IMediaItem NextItem
      {
         get
         {
            if (HasNext())
            {
               if (ShuffleMode == ShuffleMode.On)
               {
                  return this[_shuffledIndexes[_indexOfCurrentItemInShuffledIndexes + 1]];
               }
               else
               {
                  return this[CurrentIndex + 1];
               };
            }
            else
            {
               return null;
            };
         }
      }

      public Task PlayNext()
      {
         if (HasNext())
         {
            return MediaPlayer.Current.Play(NextItem);
         };

         return Task.CompletedTask;
      }

      // - - -  - - - 

      public bool HasPrevious()
      {
         if (ShuffleMode == ShuffleMode.On)
         {
            return _indexOfCurrentItemInShuffledIndexes > 0;
         }
         else
         {
            return CurrentIndex > 0;
         }
      }

      public IMediaItem PreviousItem
      {
         get
         {
            if (HasPrevious())
            {
               if (ShuffleMode == ShuffleMode.On)
               {
                  return this[_shuffledIndexes[_indexOfCurrentItemInShuffledIndexes - 1]];
               }
               else
               {
                  return this[CurrentIndex - 1];
               };
            }
            else
            {
               return null;
            }
         }
      }


      public Task PlayPrevious()
      {
         if (HasPrevious())
         {
            return MediaPlayer.Current.Play(PreviousItem);
         };

         return Task.CompletedTask;
      }

      // - - -  - - - 

      public Task InsertAfterCurrent(IMediaItem mediaItem)
      {
         if (Current == null)
         {
            ClearItems();
            MediaPlayer.Current.Play(mediaItem);
         }
         else
         {
            var ind = this.IndexOf(_Current);

            if (ind >= this.Count - 1)
            {
               this.Add(mediaItem);
            }
            else
            {
               //ToDo: shuffeled list
               this.Insert(ind + 1, mediaItem);
            };
         };

         return Task.CompletedTask;
      }

   }
}

/*
IMediaManager MediaManager = CrossMediaManager.Current;

public event QueueEndedEventHandler QueueEnded;

public event QueueChangedEventHandler QueueChanged;


public bool HasCurrent() => Count >= CurrentIndex;
public IMediaItem Current => Count > 0 ? this[CurrentIndex] : null;


internal void OnQueueEnded(object s, QueueEndedEventArgs e) => QueueEnded?.Invoke(s, e);

internal void OnQueueChanged(object s, QueueChangedEventArgs e) => QueueChanged?.Invoke(s, e);
*/
