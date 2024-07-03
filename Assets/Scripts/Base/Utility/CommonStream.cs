using System;
using Saem;
using UniRx;
namespace Saem
{
    public class CommonStream
    {
        public static IObservable<int> createCountDownObservable(int startTime, int endTime, float term) =>
            Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(term))
                .Select(x => (int)(startTime - x))
                .TakeWhile(x => x > endTime);

        public static IObservable<T> delayStream<T>(T value)
        {
            return delayStreamWithTime(value);
        }

        public static IObservable<T> delayStreamWithTime<T>(T value, float time = 2.0f)
        {
            return Observable.Timer(TimeSpan.FromSeconds(time), Scheduler.MainThreadIgnoreTimeScale).Select(_ => value);
        }
    }
}
