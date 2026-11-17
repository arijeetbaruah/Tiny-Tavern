using System;
using UnityEngine;

namespace Game.OptionsMenu
{
    public class OptionMenuSaveData : IEquatable<OptionMenuSaveData>
    {
        public VolumeTabSaveData VolumeTab = new ();
        public GraphicsTabSaveData GraphicsTab = new ();

        public bool Equals(OptionMenuSaveData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(VolumeTab, other.VolumeTab) && Equals(GraphicsTab, other.GraphicsTab);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OptionMenuSaveData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VolumeTab, GraphicsTab);
        }
    }

    public class VolumeTabSaveData : IEquatable<VolumeTabSaveData>
    {
        public float MasterVolume = 1;
        public float MusicVolume = 1;
        public float SFXVolume = 1;

        public bool Equals(VolumeTabSaveData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return MasterVolume.Equals(other.MasterVolume) && MusicVolume.Equals(other.MusicVolume) && SFXVolume.Equals(other.SFXVolume);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((VolumeTabSaveData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MasterVolume, MusicVolume, SFXVolume);
        }
    }
    
    public class GraphicsTabSaveData : IEquatable<GraphicsTabSaveData>
    {
        public int Quality = -1;
        public int ResolutionWidth = 0;
        public int ResolutionHeight = 0;
        public bool Fullscreen = true;

        public bool Equals(GraphicsTabSaveData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Quality == other.Quality && ResolutionWidth == other.ResolutionWidth && ResolutionHeight == other.ResolutionHeight && Fullscreen == other.Fullscreen;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GraphicsTabSaveData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Quality, ResolutionWidth, ResolutionHeight, Fullscreen);
        }
    }
}
