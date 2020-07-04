
let TimelineHandle = {

    newTimeline: function () {
        var timeline = {
            day: '',
            description: '',
            timelineDetail: []
        }

        return timeline;
    },

    newTimelineDetail: function (time, detail) {
        var timelineDetail = {
            time: time,
            detail: detail
        }

        return timelineDetail;
    }
};