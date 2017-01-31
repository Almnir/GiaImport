using RBD.Resources.Wrapper;

namespace RBD.Common
{
    static class ShareWrapperCommon
    {
        static ShareWrapperCommon()
        {
            Wrapper = new ShareWrapper();
        }

        public static ShareWrapper Wrapper{ get; private set;}

    }

    public static class RepositoryItems
    {
		//public static RepositoryItem Empty = new RepositoryItem();

		//public static RepositoryItemImageEdit GetImage(RbdImages image)
  //      {
  //          var imageContainer = new ImageCollection();
  //          switch (image)
  //          {
  //              case RbdImages.Lock:
  //                  imageContainer.AddImage(ShareWrapperCommon.Wrapper.GetResorses<Bitmap>("_lock"));
  //                  break;
  //              case RbdImages.Delete:
  //                  imageContainer.AddImage(ShareWrapperCommon.Wrapper.GetResorses<Bitmap>("delete"));
  //                  break;
		//		case RbdImages.FlagRed:
  //                  imageContainer.AddImage(ShareWrapperCommon.Wrapper.GetResorses<Bitmap>("flag_red"));
		//			break;
		//	}

  //          return new RepositoryItemImageEdit()
  //          {
  //              Images = imageContainer,
  //              ReadOnly = true,
  //              ShowDropDown = ShowDropDown.Never
  //          };
  //      }
	}
}
